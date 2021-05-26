import React from 'react';
import Content from '../../Content';
import { shallow } from 'enzyme';
import configureMockStore from 'redux-mock-store';
import { SET_PARAM } from '../../../actions/types';

const mockStore = configureMockStore();

describe('registerd member list - Content - connect', () => {
    let wrapper;
    let store;

    beforeEach(() => {
        const initialState = {
            param: {},
            isShowMemberName: false
        };
        store = mockStore(initialState);
        wrapper = shallow(<Content store={store} />).dive();
    });

    it('should show correct value', () => {
        expect(wrapper.props().isShowMemberName).toBeFalsy();
    });

    it('should call correct action', () => {
        wrapper.props().setParam({});

        const actions = store.getActions();
        expect(actions).toEqual([{ type: SET_PARAM, data: {} }]);
    });
})