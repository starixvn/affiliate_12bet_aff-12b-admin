import { combineReducers } from 'redux';

import * as filterReducer from './filterReducer';

export default combineReducers(
    Object.assign({}, {
    ...filterReducer,
}));    