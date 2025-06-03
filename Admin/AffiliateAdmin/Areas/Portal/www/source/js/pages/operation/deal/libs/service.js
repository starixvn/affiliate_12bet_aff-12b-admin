
const commonDealInfo = {
    DealId: 0,
    DealName: '',
    DealType: 3,
    Description: '',
    SubCommRate: 0.05,
    TargetCountry: ''
}

export const buildDefaultDealInfo = () => {
    return Object.assign({}, commonDealInfo);
}