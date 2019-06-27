import { setShowPopupPassword, updateChangePasswword } from '../changePassword';
import * as ajaxRequest from '../../../../modules/ajaxRequest';
import { SET_SHOW_POPUP_PASSWORD } from '../types';

describe('changePassword actions', () => {
    it(`should create action to setShowPopupPassword action`, async () => {
        const expectedAction = { type: SET_SHOW_POPUP_PASSWORD, data: true };

        expect(setShowPopupPassword(true)).toEqual(expectedAction);
    });

    it('should create action to updateChangePasswword with incorrect parameter', async () => {
        ajaxRequest.post = jest.fn();
        const param = {
            AffId: 123456,
            Password: '123abc',
            ConfirmPassword: '123abc'
        }

        const result = await updateChangePasswword(param)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/AffiliateInfo/ChangePassword', param);
        expect(result).toEqual(null);
    })

    it('should create action to updateChangePasswword with correct parameter', async () => {
        const expectedResponse = { data: { IsSuccessful: true, Message: '' }, status: 200 };
        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));
        const param = {
            AffId: 968222,
            Password: '123abc',
            ConfirmPassword: '123abc'
        }
        
        const result = await updateChangePasswword(param)();

        expect(ajaxRequest.post).toHaveBeenCalledWith('Portal/AffiliateInfo/ChangePassword', param);
        expect(result).toEqual({ IsSuccessful: true, Message: '' });
    })
});