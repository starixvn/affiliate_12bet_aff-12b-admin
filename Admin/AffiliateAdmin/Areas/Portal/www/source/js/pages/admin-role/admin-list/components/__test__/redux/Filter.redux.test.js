import React from 'react';
import Filter from '../../Filter';
import { shallow } from 'enzyme';
import configureMockStore from 'redux-mock-store';
import { SET_PARAM } from '../../../actions/types';

const mockStore = configureMockStore();

describe('admin list - components - Filter - connect', () => {
    let wrapper;
    let store;

    beforeEach(() => {
        const initialState = {
            adminList: {
                isBack: false
            },
            hasWriteAdminRoleClaim: true
        };
        store = mockStore(initialState);
        wrapper = shallow(<Filter store={store} />).dive();
    });

    it('should show correct value', () => {
        expect(wrapper.props().isBack).toBeFalsy();
    });

    it('should call correct action', () => {
        wrapper.props().setParam({});

        const actions = store.getActions();
        expect(actions).toEqual([{ type: SET_PARAM, data: {} }]);
    });
})