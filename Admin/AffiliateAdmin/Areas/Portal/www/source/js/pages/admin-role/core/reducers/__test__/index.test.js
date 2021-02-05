import { RESET_REDUCER } from '../../actions/types';
import { rootReducer } from '../index';

describe('admin role - core - reducer', () => {
    const expectedResult = {
        adminInfo: {
            marketListByUser: null,
            permissionListByRole: null,
            roleList: null
        },
        adminList: {
            checkedUsers: {},
            gridData: null,
            param: null,
            isBack: false,
            userInfo: null,
            shouldResetScrollPosition: true
        },
        selectedMenu: "adminInfo",
        isShowLoader: false,
        userId: null,
        userName: null,
        hasWriteAdminRoleClaim: false
    }
    it(`should handle rootReducer reducer`, () => {
        const expectResult = Object.assign({}, expectedResult, { isShowLoader: true, })
        const state = { selectedMenu: 'adminInfo' };
        const result = rootReducer(state, { type: 'TOGGLE_LOADER', data: true });
        expect(result).toEqual(expectResult);
    });

    it(`should handle rootReducer reducer with action.type is RESET_REDUCER`, () => {
        const state = { selectedMenu: 'adminInfo' };
        const result = rootReducer(state, { type: RESET_REDUCER, data: 'adminList' });
        expect(result).toEqual(expectedResult);
    });
});