import React from 'react';
import GeneralComponent from '../../GeneralComponent';
import { shallow } from 'enzyme';
import configureMockStore from 'redux-mock-store';
import { SET_GRID_DATA } from '../../../actions/types'

const mockStore = configureMockStore();

describe('role permission - components - GeneralComponent - connect', () => {
    let wrapper;
    let store;

    beforeEach(() => {
        const initialState = {
            hasWriteAdminRoleClaim: true
        };
        store = mockStore(initialState);
        wrapper = shallow(<GeneralComponent store={store} />).dive();
    });

    it('should show correct value', () => {
        expect(wrapper.props().hasWriteAdminRoleClaim).toBeTruthy();
    });

    it('should call correct action', () => {
        wrapper.props().setGridData(null);

        const actions = store.getActions();
        expect(actions).toEqual([{ type: SET_GRID_DATA, data: null }]);
    });
})