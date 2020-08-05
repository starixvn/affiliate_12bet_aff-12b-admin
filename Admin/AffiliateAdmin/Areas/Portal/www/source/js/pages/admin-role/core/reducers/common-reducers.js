import { isShowLoader } from '../../../../reducers/loaderReducer';
import createReducer from '../../../../modules/createReducer';
import { SET_USER_ID, SET_USER_NAME } from '../actions/types';

export { isShowLoader }

export const userId = createReducer(null, {
    [SET_USER_ID](prevState, action) {
        return action.data;
    }
})

export const userName = createReducer(null, {
    [SET_USER_NAME](prevState, action) {
        return action.data;
    }
})