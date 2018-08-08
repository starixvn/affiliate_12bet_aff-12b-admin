import { formatDecimal } from "../../../modules/numberFormat";

export const INFINITY_AM = 100000000000000;
export const INFINITY_QM = 1000000000;
export const INFINITY_SYMBOL = '∞';

export const defaultPagingOption = {
    PageIndex: 1,
    PageSize: 20,
    TotalItem: 0,
    PageList: [10, 20, 50, 100, 500]
};

function generateCommissionValues() {
    let min = 0,
        max = 1;
    const commissionList = ['0.00'];

    while (min < max) {
        min = min + 0.01;
        commissionList.push(formatDecimal(min));
    }

    return commissionList;
}
const _commValueList = generateCommissionValues();

export const commissionValueList = _commValueList;