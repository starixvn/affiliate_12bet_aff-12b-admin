import React from 'react';
import Content from '../../Content';
import { shallow } from 'enzyme';
import configureMockStore from 'redux-mock-store';
import { SET_PERMISSION_LIST_BY_ROLE } from '../../../actions/types';

const mockStore = configureMockStore();

describe('admin info - components - Content - connect', () => {
    let wrapper;
    let store;

    beforeEach(() => {
        const initialState = {
            userId: 123,
            userName: "ABC",
            adminInfo: { roleList: [], permissionListByRole: [] },
            hasWriteAdminRoleClaim: true
        };
        store = mockStore(initialState);
        wrapper = shallow(<Content store={store} />).dive();
    });

    it('should show correct value', () => {
        expect(wrapper.props().userId).toBe(123);
        expect(wrapper.props().userName).toEqual("ABC");
        expect(wrapper.props().roleList).toEqual([]);
        expect(wrapper.props().permissionListByRole).toEqual([]);
        expect(wrapper.props().hasWriteAdminRoleClaim).toBeTruthy();
    });

    it('should call correct action', () => {
        wrapper.props().setPermissionListByRole([]);

        const actions = store.getActions();
        expect(actions).toEqual([{ type: SET_PERMISSION_LIST_BY_ROLE, data: [] }]);
    });
})