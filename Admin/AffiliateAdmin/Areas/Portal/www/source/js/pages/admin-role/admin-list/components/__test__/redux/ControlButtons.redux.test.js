import React from 'react';
import ControlButtons from '../../ControlButtons';
import { shallow } from 'enzyme';
import configureMockStore from 'redux-mock-store';
import { TOGGLE_LOADER } from '../../../../../../actions/actionTypes';

const mockStore = configureMockStore();

describe('admin list - components - ControlButtons - connect', () => {
    let wrapper;
    let store;

    beforeEach(() => {
        const initialState = {
            adminList: {
                param: {},
                checkedUsers: '1,2,3',
                gridData: []
            },
            hasWriteAdminRoleClaim: true
        };
        store = mockStore(initialState);
        wrapper = shallow(<ControlButtons store={store} />).dive();
    });

    it('should show correct value', () => {
        expect(wrapper.props().param).toEqual({});
        expect(wrapper.props().checkedUsers).toEqual('1,2,3');
        expect(wrapper.props().gridData).toEqual([]);
    });

    it('should call correct action', async () => {
        await wrapper.props().toggleLoader(true);

        const actions = store.getActions();
        expect(actions).toEqual([{ type: TOGGLE_LOADER, data: true }]);
    });
})