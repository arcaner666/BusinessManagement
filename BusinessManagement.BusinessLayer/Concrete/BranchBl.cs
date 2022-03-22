using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete
{
    public class BranchBl
    {
        private readonly IBranchDal _branchDal;

        public BranchBl(
            IBranchDal branchDal
        )
        {
            _branchDal = branchDal;
        }

        public IDataResult<BranchDto> Add(BranchDto branchDto)
        {
            Branch getBranch = _branchDal.GetByBusinessIdAndBranchOrderOrBranchCode(branchDto.BusinessId, branchDto.BranchOrder, branchDto.BranchCode);
            if (getBranch != null)
            {
                return new ErrorDataResult<BranchDto>(Messages.BranchAlreadyExists);
            }

            Branch addBranch = new()
            {
                BusinessId = branchDto.BusinessId,
                FullAddressId = branchDto.FullAddressId,
                BranchOrder = 1,
                BranchName = "Merkez",
                BranchCode = "000001",
                CreatedAt = DateTimeOffset.Now,
                UpdatedAt = DateTimeOffset.Now,
            };
            _branchDal.Add(addBranch);

            BranchDto addBranchDto = FillDto(addBranch);

            return new SuccessDataResult<BranchDto>(addBranchDto, Messages.BranchAdded);
        }
        
        private BranchDto FillDto(Branch branch)
        {
            BranchDto branchDto = new()
            {
                BranchId = branch.BranchId,
                BusinessId = branch.BusinessId,
                FullAddressId = branch.FullAddressId,
                BranchOrder = branch.BranchOrder,
                BranchName = branch.BranchName,
                BranchCode = branch.BranchCode,
                CreatedAt = branch.CreatedAt,
                UpdatedAt = branch.UpdatedAt,
            };

            return branchDto;
        }

        private List<BranchDto> FillDtos(List<Branch> branches)
        {
            List<BranchDto> branchDtos = branches.Select(branch => FillDto(branch)).ToList();

            return branchDtos;
        }
    }
}
