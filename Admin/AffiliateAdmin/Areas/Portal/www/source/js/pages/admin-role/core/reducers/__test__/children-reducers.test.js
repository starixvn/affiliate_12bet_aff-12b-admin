import { SET_SELECTED_MENU } from '../../actions/types';
import { selectedMenu } from '../children-reducers';

describe('admin role - core - children reducer', () => {
    it(`should handle selectedMenu reducer with type SET_USER_ID`, () => {
        const result = selectedMenu('adminList', { type: SET_SELECTED_MENU, data: 'adminInfo' });
        expect(result).toEqual('adminInfo');
    });
});