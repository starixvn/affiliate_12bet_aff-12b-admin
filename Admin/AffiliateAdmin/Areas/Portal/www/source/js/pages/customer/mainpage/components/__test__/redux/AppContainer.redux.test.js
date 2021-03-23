import React from 'react';
import AppContainer from '../../AppContainer';
import { shallow } from 'enzyme';
import configureMockStore from 'redux-mock-store';
import { SET_SELECTED_USERPANEL } from '../../../../user-panel/actions/types';

const mockStore = configureMockStore();

describe('banner list - components - AppContainer - connect', () => {
    let wrapper;
    let store;

    beforeEach(() => {
        const initialState = {
            selectedUserPanel: "Statement History"
        };
        store = mockStore(initialState);
        wrapper = shallow(<AppContainer store={store} />).dive();
    });

    it('should show correct value', () => {
        expect(wrapper.props().selectedUserPanel).toEqual("Statement History");
    });

    it('should call correct action', () => {
        wrapper.props().setSelectedUserPanel("ABC");

        const actions = store.getActions();
        expect(actions).toEqual([{ type: SET_SELECTED_USERPANEL, data: "ABC" }]);
    });
})