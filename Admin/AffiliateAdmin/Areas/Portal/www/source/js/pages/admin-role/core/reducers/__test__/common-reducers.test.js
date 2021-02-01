import { SET_USER_ID, SET_USER_NAME } from '../../actions/types';
import { userId, userName } from '../common-reducers';

describe('admin role - core - common reducer', () => {
    it(`should handle userId reducer with type SET_USER_ID`, () => {
        const result = userId(null, { type: SET_USER_ID, data: 123 });
        expect(result).toEqual(123);
    });

    it(`should handle userName reducer with type SET_USER_NAME`, () => {
        const result = userName(null, { type: SET_USER_NAME, data: 'Abc' });
        expect(result).toEqual('Abc');
    });
});