import createReducer from '../../../../modules/createReducer';
import { SET_SELECTED_USERPANEL } from '../actions/types';

export const selectedUserPanel = createReducer(null, {
    [SET_SELECTED_USERPANEL](prevState, action) {
        return action.data;
    }
});