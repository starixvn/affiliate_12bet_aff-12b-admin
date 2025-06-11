
const commonDealInfo = {
    DealId: 0,
    DealName: '',
    DealType: 3,
    Description: '',
    SubCommRate: 0.05,
    CountryCodes: '',
    ActiveDays: '',
    IsActivePlayerHoldingPeriod: false,
    HoldingTickets: 0,
    HoldingDays: 0,
    IsQuota: false,
    IsInstantPayment: false,
    Quota: 0,
    CheckedMinFdm: false,
    CheckedMinTotalDeposit: false,
    CheckedMinWagering: false,
    CPATierSetting: [
        {
            TierId: 1,
            GroupProductId: 1,
            MinFdm: 0,
            MinTotalDeposit: 0,
            MinWagering: 0,
            CpaAmount: 1
        }
    ],
    ProductGroup: [
        {
            ProductIds: '',
            GroupProductId: 1,
        },
    ]
}

export const buildDefaultDealInfo = () => {
    return Object.assign({}, commonDealInfo);
}

export const convertProductList = (productList, productIds = '') => {
    const idList = productIds.split(',').filter(p => !!p);
    const isCheckAll = idList.length === productList.length;

    return productList.map(item => {
        return {
            ProductId: item.ProductId,
            ProductName: item.ProductName,
            Checked: isCheckAll || idList.filter(i => Number(i) === item.ProductId).length > 0
        }
    })
}