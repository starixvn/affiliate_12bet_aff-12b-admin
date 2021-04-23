import React from 'react';
import Filter from '../../Filter';
import { shallow } from 'enzyme';
import configureMockStore from 'redux-mock-store';
import { SET_MAIN_VIEW_PARAM } from '../../../actions/types';

const mockStore = configureMockStore();

describe('News View Log - components - Filter - connect', () => {
    let wrapper;
    let store;

    beforeEach(() => {
        const initialState = {
            today: '4/2/2021',
            selectedAff: '',
            detailViewParam: {},
            mainViewParam: {}
        };
        store = mockStore(initialState);
        wrapper = shallow(<Filter store={store} />).dive();
    });

    it('should show correct value', () => {
        expect(wrapper.props().today).toEqual('4/2/2021');
        expect(wrapper.props().selectedAff).toEqual('');
        expect(wrapper.props().detailViewParam).toEqual({});
        expect(wrapper.props().mainViewParam).toEqual({});
    });

    it('should call correct action', () => {
        wrapper.props().setMainViewParam(null);

        const actions = store.getActions();
        expect(actions).toEqual([{ type: SET_MAIN_VIEW_PARAM, data: null }]);
    });
})