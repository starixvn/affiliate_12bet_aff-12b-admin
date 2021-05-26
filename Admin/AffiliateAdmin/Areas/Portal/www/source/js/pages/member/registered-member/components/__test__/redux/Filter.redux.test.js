import React from 'react';
import Filter from '../../Filter';
import { shallow } from 'enzyme';
import configureMockStore from 'redux-mock-store';
import { SET_PARAM } from '../../../actions/types';

const mockStore = configureMockStore();

describe('registerd member list - Filter - connect', () => {
    let wrapper;
    let store;

    beforeEach(() => {
        const initialState = {
            pagingOption: { PageIndex: 1 }
        };
        store = mockStore(initialState);
        wrapper = shallow(<Filter store={store} />).dive();
    });

    it('should show correct value', () => {
        expect(wrapper.props().pagingOption.PageIndex).toBe(1);
    });

    it('should call correct action', () => {
        wrapper.props().setParam({});

        const actions = store.getActions();
        expect(actions).toEqual([{ type: SET_PARAM, data: {} }]);
    });
})