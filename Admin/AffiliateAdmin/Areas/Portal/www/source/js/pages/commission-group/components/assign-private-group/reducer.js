import { combineReducers } from "redux";
import { selectedGroup, productList, p2pProductList, deductionList, defaultCurrency, selectedProductIds } from '../../reducers';
import createReducer from '../../../../modules/createReducer';

export const affInfo = createReducer({}, {});
export const selectedGroupType = createReducer(1, {});

export default combineReducers({
    productList,
    p2pProductList,
    deductionList,
    selectedGroup,
    affInfo,
    defaultCurrency,
    selectedProductIds,
    selectedGroupType
});