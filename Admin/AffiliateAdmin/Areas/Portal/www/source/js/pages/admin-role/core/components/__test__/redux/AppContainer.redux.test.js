import React from 'react';
import { shallow } from 'enzyme';
import configureMockStore from 'redux-mock-store';
import AppContainer from '../../AppContainer';
import { SET_SELECTED_MENU } from '../../../actions/types';

const mockStore = configureMockStore();

describe('admin role - core - components - AppContainer - connect', () => {
    let store;
    let wrapper;

    beforeEach(() => {
        const initialState = {
            isShowLoader: false,
            selectedMenu: 'adminList'
        };

        store = mockStore(initialState);
        wrapper = shallow(<AppContainer store={store} />).dive();
    });

    it('should mapping correct data', () => {
        expect(wrapper.props().isShowLoader).toBeFalsy();
        expect(wrapper.props().selectedMenu).toEqual('adminList');
    });

    it('should call correct action', () => {
        wrapper.props().setSelectedMenu('test');

        const action = store.getActions();
        expect(action).toEqual([{ type: SET_SELECTED_MENU, data: 'test' }]);
    })
});