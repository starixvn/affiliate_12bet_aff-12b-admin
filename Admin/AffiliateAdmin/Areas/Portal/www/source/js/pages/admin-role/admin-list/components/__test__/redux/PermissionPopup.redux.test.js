import React from 'react';
import PermissionPopup from '../../PermissionPopup';
import { shallow } from 'enzyme';
import configureMockStore from 'redux-mock-store';
import { SET_USER_INFO } from '../../../actions/types';

const mockStore = configureMockStore();

describe('admin list - components - Permission Popup - connect', () => {
    let store;
    let wrapper;

    beforeEach(() => {
        const initialState = {
            adminList: {
                userInfo: {}
            }
        };

        store = mockStore(initialState);
        wrapper = shallow(<PermissionPopup store={store} />).dive();
    });

    it('should mapping correct data', () => {
        expect(wrapper.props().userInfo).toEqual({});
    });

    it('should call correct action', () => {
        wrapper.props().setUserInfo(null);

        const action = store.getActions();
        expect(action).toEqual([{ type: SET_USER_INFO, data: null }]);
    })
})