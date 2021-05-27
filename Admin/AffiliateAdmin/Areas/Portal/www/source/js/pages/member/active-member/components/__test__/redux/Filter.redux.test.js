import React from 'react';
import Filter from '../../Filter';
import { shallow } from 'enzyme';
import configureMockStore from 'redux-mock-store';
import { SET_USER_NAMES } from '../../../actions/types';

const mockStore = configureMockStore();

describe('active member list - Filter - connect', () => {
    let wrapper;
    let store;

    beforeEach(() => {
        store = mockStore();
        wrapper = shallow(<Filter store={store} />).dive();
    });

    it('should call correct action', () => {
        wrapper.props().setUserNames('abc');

        const actions = store.getActions();
        expect(actions).toEqual([{ type: SET_USER_NAMES, data: 'abc' }]);
    });
})