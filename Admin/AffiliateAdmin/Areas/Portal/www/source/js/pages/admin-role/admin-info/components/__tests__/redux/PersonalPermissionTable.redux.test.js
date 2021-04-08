import React from 'react';
import PersonalPermissionTable from '../../PersonalPermissionTable';
import { shallow } from 'enzyme';
import configureMockStore from 'redux-mock-store';
import { SET_PERMISSION_LIST_BY_ROLE } from '../../../actions/types';

const mockStore = configureMockStore();

describe('admin info - components - PersonalPermissionTable - connect', () => {
    let wrapper;
    let store;

    beforeEach(() => {
        const initialState = {
            adminInfo: { permissionListByRole: [] }
        };
        store = mockStore(initialState);
        wrapper = shallow(<PersonalPermissionTable store={store} />).dive();
    });

    it('should show correct value', () => {
        expect(wrapper.props().permissionListByRole).toEqual([]);
    });

    it('should call correct action', () => {
        wrapper.props().setPermissionListByRole(null);

        const actions = store.getActions();
        expect(actions).toEqual([{ type: SET_PERMISSION_LIST_BY_ROLE, data: null }]);
    });
})