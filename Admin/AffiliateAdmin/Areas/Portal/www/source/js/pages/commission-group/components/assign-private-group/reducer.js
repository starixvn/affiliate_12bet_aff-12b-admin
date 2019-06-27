import { combineReducers } from "redux";
import { selectedGroup, productList, deductionList } from '../../reducers';
import createReducer from '../../../../modules/createReducer';

export const affInfo = createReducer({}, {});

export default combineReducers({ productList, deductionList, selectedGroup, affInfo });