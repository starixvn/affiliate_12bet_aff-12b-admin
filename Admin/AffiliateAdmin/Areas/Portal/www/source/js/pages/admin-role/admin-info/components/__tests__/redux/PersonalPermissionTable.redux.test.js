import React from 'react';
import PersonalPermissionTable from '../../PersonalPermissionTable';
import { shallow } from 'enzyme';
import configureMockStore from 'redux-mock-store';

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
})