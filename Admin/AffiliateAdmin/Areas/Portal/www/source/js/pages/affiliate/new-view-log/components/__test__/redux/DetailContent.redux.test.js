import React from 'react';
import DetailContent from '../../DetailContent';
import { shallow } from 'enzyme';
import configureMockStore from 'redux-mock-store';
import { SET_SELECTED_AFF } from '../../../actions/types';

const mockStore = configureMockStore();

describe('News View Log - components - DetailContent - connect', () => {
    let wrapper;
    let store;

    beforeEach(() => {
        const initialState = {
            selectedAff: '',
            detailViewParam: {},
            pagingOption: {}
        };
        store = mockStore(initialState);
        wrapper = shallow(<DetailContent store={store} />).dive();
    });

    it('should show correct value', () => {
        expect(wrapper.props().selectedAff).toEqual('');
        expect(wrapper.props().pagingOption).toEqual({});
        expect(wrapper.props().detailViewParam).toEqual({});
    });

    it('should call correct action', () => {
        wrapper.props().setSelectedAff('abc');

        const actions = store.getActions();
        expect(actions).toEqual([{ type: SET_SELECTED_AFF, data: 'abc' }]);
    });
})