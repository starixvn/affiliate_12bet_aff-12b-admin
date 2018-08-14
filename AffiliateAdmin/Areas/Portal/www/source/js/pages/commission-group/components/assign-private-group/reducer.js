import { selectedGroup, productList, deductionList } from '../../reducers';
import { combineReducers } from "redux";

export default combineReducers({ productList, deductionList, selectedGroup });