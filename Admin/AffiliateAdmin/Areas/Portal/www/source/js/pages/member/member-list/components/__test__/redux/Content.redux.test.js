import React from 'react';
import Content from '../../Content';
import { shallow } from 'enzyme';
import configureMockStore from 'redux-mock-store';
import { SET_PARAM } from '../../../actions/types';

const mockStore = configureMockStore();

describe('member list - Content - connect', () => {
    let wrapper;
    let store;

    beforeEach(() => {
        const initialState = {
            claimsPrincipal: { hasWriteClaim: jest.fn(() => false), hasClaim: jest.fn(() => false) },
            param: {}
        };
        store = mockStore(initialState);
        wrapper = shallow(<Content store={store} />).dive();
    });

    it('should show correct value', () => {
        expect(wrapper.props().isModifiable).toBeFalsy();
        expect(wrapper.props().isModifiable).toBeFalsy();
    });

    it('should call correct action', () => {
        wrapper.props().setParam({});

        const actions = store.getActions();
        expect(actions).toEqual([{ type: SET_PARAM, data: {} }]);
    });
})