import { combineReducers } from "redux";
import { selectedGroup, productList, deductionList } from '../../reducers';
import createReducer from '../../../../modules/createReducer';

const paymentRequestInfo = createReducer({}, {});

export default combineReducers({ paymentRequestInfo });