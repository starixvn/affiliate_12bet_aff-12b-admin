import createReducer from '../../../../modules/createReducer';
import { SET_SELECTED_MENU } from '../actions/types';
import { adminRoleMenuKey as menuKey } from '../libs/constants';

import adminListReducer from '../../admin-list/reducers';
import adminInfoReducer from '../../admin-info/reducers';

export const selectedMenu = createReducer('adminList', {
    [SET_SELECTED_MENU](prevState, action) {
        return action.data;
    }
});

export const adminRoleReducer = {
    [menuKey.adminList]: adminListReducer,
    [menuKey.adminInfo]: adminInfoReducer,
}