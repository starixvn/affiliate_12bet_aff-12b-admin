import React from 'react';
import Filter from '../../Filter';
import { shallow } from 'enzyme';
import configureMockStore from 'redux-mock-store';
import { SET_REPORT_TITLE } from '../../../actions/types';

const mockStore = configureMockStore();

describe('Report - components - Filter - connect', () => {
    let wrapper;
    let store;

    beforeEach(() => {
        const initialState = {
            today: '4/2/2021',
            claimsPrincipal: { hasReadClaim: jest.fn(() => true) }
        };
        store = mockStore(initialState);
        wrapper = shallow(<Filter store={store} />).dive();
    });

    it('should show correct value', () => {
        expect(wrapper.props().today).toEqual('4/2/2021');
        expect(wrapper.props().hasTurnOverPermission).toTruthy;
        expect(wrapper.props().isShowLangFilter).toTruthy;
    });

    it('should call correct action', () => {
        wrapper.props().setReportTitle(null);

        const actions = store.getActions();
        expect(actions).toEqual([{ type: SET_REPORT_TITLE, data: null }]);
    });
})