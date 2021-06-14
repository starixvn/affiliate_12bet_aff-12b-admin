import React from 'react';
import UploadResult from '../../UploadResult';
import { shallow } from 'enzyme';
import configureMockStore from 'redux-mock-store';
import { SET_IS_CLICK_UPLOAD } from '../../../actions/types';

const mockStore = configureMockStore();

describe('mass adjustment - components - ImportForm - connect', () => {
    let wrapper;
    let store;

    beforeEach(() => {
        const initialState = {
            file: { name: 'abc.xlsx' },
            isClickUpload: true
        };
        store = mockStore(initialState);
        wrapper = shallow(<UploadResult store={store} />).dive();
    });

    it('should show correct value', () => {
        expect(wrapper.props().file).toEqual({ name: 'abc.xlsx' });
    });

    it('should call correct action', () => {
        wrapper.props().setIsClickUpload(false);

        const actions = store.getActions();
        expect(actions).toEqual([{ type: SET_IS_CLICK_UPLOAD, data: false }]);
    });
})