import { validateCommGroup } from '../utils';

describe('commission group - utils', () => {
    const selectedGroup = {
        GroupId: 102,
        BankingFee: { DeductedPercentage: 0.02, DeductionId: 12, ProductIds: 0 },
        Description: "",
        GroupId: 0,
        GroupName: "",
        OperationalFee: { DeductedPercentage: 0.07, DeductionId: 11, ProductIds: 0 },
        PlatformFeeList: [{ DeductedPercentage: 0.17, DeductionId: 10, ProductIds: "1,3,4,5,6" }],
        QmTurnover: 100,
        TierSettingList: [{ TierId: 1, MinAmount: 0, MaxAmount: 100000000000000, MinQm: 0, MaxQm: 1000000000, Commission: 0 }],
        FdmSettingList: [{ Commission: 0, MaxExtraComm: 100, MaxFdmDeposit: 100000000000000, MinFdmDeposit: 0, MaxNewFdm: 1000000000, MinNewFdm: 0, FdmId: 1 }],
        IsApplyBonus: true
    };

    it('validateCommGroup', () => {
        const result = validateCommGroup(selectedGroup, false);

        expect(result).toEqual({ isValid: true, messageList: [] });
    })

    it('validateCommGroup - QmTurnover is NaN', async () => {
        const selected_Group = Object.assign({}, selectedGroup, { QmTurnover: 'abc' })

        const result = validateCommGroup(selected_Group, false);

        expect(result).toEqual({ isValid: false, messageList: ["QM Turnover value is required"] });
    })

    it('validateCommGroup - TierSettingList is not MaxAmount', () => {
        const selected_Group = Object.assign({}, selectedGroup, {
            TierSettingList: [{ TierId: 1, MinAmount: 0, MaxAmount: 100, MinQm: 0, MaxQm: 10, Commission: 0 }],
            FdmSettingList: [{ Commission: 0, MaxExtraComm: 100, MaxFdmDeposit: 0, MinFdmDeposit: 0, MaxNewFdm: 0, MinNewFdm: 0, FdmId: 1 }]
        })

        const result = validateCommGroup(selected_Group, false);

        expect(result).toEqual({ isValid: false, messageList: ["Last Tier Setting must have maximum value=∞", "Last FDM Setting must have maximum value=∞"] });
    })

    it('validateCommGroup - TierSettingList has MinAmount > MaxAmount', () => {
        const selected_Group = Object.assign({}, selectedGroup, {
            TierSettingList: [{ TierId: 1, MinAmount: 1000000000000000, MaxAmount: 100000000000000, MinQm: 0, MaxQm: 1000000000, Commission: 0 }],
            FdmSettingList: [{ Commission: 0, MaxExtraComm: 100, MaxFdmDeposit: 100000000000000, MinFdmDeposit: 1000000000000000, MaxNewFdm: 1000000000, MinNewFdm: 0, FdmId: 1 }]
        })

        const result = validateCommGroup(selected_Group, false);

        expect(result).toEqual({ isValid: false, messageList: ["Max Amount must be greater than or equal to Min Amount", "Max FDM's Deposit must be greater than or equal to Min FDM's Deposit"] });
    })

    it('validateCommGroup - TierSettingList has MinQm > MaxQm', () => {
        const selected_Group = Object.assign({}, selectedGroup, {
            TierSettingList: [{ TierId: 1, MinAmount: 10, MaxAmount: 100000000000000, MinQm: 10000000000, MaxQm: 1000000000, Commission: 0 }],
            FdmSettingList: [{ Commission: 0, MaxExtraComm: 100, MaxFdmDeposit: 100000000000000, MinFdmDeposit: 1000000000000000, MaxNewFdm: 1000000000, MinNewFdm: 10000000000, FdmId: 1 }]
        })

        const result = validateCommGroup(selected_Group, false);

        expect(result).toEqual({ isValid: false, messageList: ["Max QM must be greater than or equal to Min QM", "Max New FDM must be greater than or equal to Min New FDM"] });
    })

    it('validateCommGroup - PlatformFeeList has ProductId is null', () => {
        const selected_Group = Object.assign({}, selectedGroup, { PlatformFeeList: [{ DeductedPercentage: 0.17, DeductionId: 10, ProductIds: "" }] })

        const result = validateCommGroup(selected_Group, false);

        expect(result).toEqual({ isValid: false, messageList: ["At least 1 product must be selected per group"] });
    })
})