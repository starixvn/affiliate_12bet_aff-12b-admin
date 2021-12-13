import React from 'react';
import Filter from '../../Filter';
import { shallow } from 'enzyme';
import configureMockStore from 'redux-mock-store';
import { SET_REPORT_TITLE } from '../../../actions/types';

const mockStore = configureMockStore();

describe('processing request - components - Filter - connect', () => {
    let wrapper;
    let store;

    beforeEach(() => {
        const initialState = {
            param: {},
            pagingOption: {}
        };
        store = mockStore(initialState);
        wrapper = shallow(<Filter store={store} />).dive();
    });

    it('should show correct value', () => {
        expect(wrapper.props().param).toEqual({});
        expect(wrapper.props().pagingOption).toEqual({});
    });

    it('should call correct action', () => {
        wrapper.props().setReportTitle("abc");

        const actions = store.getActions();
        expect(actions).toEqual([{ type: SET_REPORT_TITLE, data: "abc" }]);
    });
})