import React from 'react';
import AppContainer from '../../AppContainer';
import { shallow } from 'enzyme';
import configureMockStore from 'redux-mock-store';
import { SET_MESSAGE } from '../../../actions/types';

const mockStore = configureMockStore();

describe('active member - components - AppContainer - connect', () => {
    let wrapper;
    let store;

    beforeEach(() => {
        const initialState = {
            isShowLoader: true
        };
        store = mockStore(initialState);
        wrapper = shallow(<AppContainer store={store} />).dive();
    });

    it('should show correct value', () => {
        expect(wrapper.props().isShowLoader).toBeTruthy();
    });

    it('should call correct action', () => {
        wrapper.props().setMessage(false);

        const actions = store.getActions();
        expect(actions).toEqual([{ type: SET_MESSAGE, data: false }]);
    });
})