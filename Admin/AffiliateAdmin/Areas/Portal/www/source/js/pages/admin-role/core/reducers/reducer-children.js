import createReducer from '../../../../modules/createReducer';
import { SET_SELECTED_MENU } from '../actions/types';
import { adminRoleMenuKey as menuKey } from '../libs/constants';

import adminUsersReducer from '../../admin-users/reducers';
import adminInfoReducer from '../../admin-info/reducers';
import permissionRoleReducer from '../../permission-role/reducers';

export const selectedMenu = createReducer(null, {
    [SET_SELECTED_MENU](prevState, action) {
        return action.data;
    }
});

export const adminRoleReducer = {
    [menuKey.adminUsers]: adminUsersReducer,
    [menuKey.adminInfoReducer]: adminInfoReducer,
    [menuKey.permissionRole]: permissionRoleReducer
}