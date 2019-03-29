import { combineReducers } from "redux";
import createReducer from '../../../../modules/createReducer';

const paymentRequestInfo = createReducer({}, {});

export default combineReducers({ paymentRequestInfo });