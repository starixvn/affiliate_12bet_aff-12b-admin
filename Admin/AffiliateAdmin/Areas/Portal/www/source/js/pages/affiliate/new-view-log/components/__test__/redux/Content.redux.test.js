import React from 'react';
import Content from '../../Content';
import { shallow } from 'enzyme';
import configureMockStore from 'redux-mock-store';
import { SET_SELECTED_AFF } from '../../../actions/types';

const mockStore = configureMockStore();

describe('News View Log - components - Content - connect', () => {
    let wrapper;
    let store;

    beforeEach(() => {
        const initialState = {
            mainViewParam: {},
            pagingOption: {}
        };
        store = mockStore(initialState);
        wrapper = shallow(<Content store={store} />).dive();
    });

    it('should show correct value', () => {
        expect(wrapper.props().pagingOption).toEqual({});
        expect(wrapper.props().mainViewParam).toEqual({});
    });

    it('should call correct action', () => {
        wrapper.props().setSelectedAff('abc');

        const actions = store.getActions();
        expect(actions).toEqual([{ type: SET_SELECTED_AFF, data: 'abc' }]);
    });
})