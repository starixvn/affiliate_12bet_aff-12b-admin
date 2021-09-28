import React from 'react';
import { resetAllProps } from '../../resetAllProps';
import { shallow } from 'enzyme';
import configureMockStore from 'redux-mock-store'
import { RESET_REDUCER } from '../../../actions/types';

const mockStore = configureMockStore()

describe('admin role - core - resetAllProps - connect', () => {
    let wrapper, store

    beforeEach(() => {
        const Test = () => (<h1>Test</h1>)
        const Item = resetAllProps(<Test />, 'test');

        const initialState = {
            resetReducer: jest.fn()
        }
        store = mockStore(initialState)
        wrapper = shallow(<Item store={store} />).dive()
    })

    it('should show correct value', () => {
        expect(wrapper.props().name).toEqual('test')
    })

    it('should call correct action', () => {
        wrapper.props().resetReducer('abc')

        const actions = store.getActions();
        expect(actions).toEqual([{ type: RESET_REDUCER, data: 'abc' }])
    })
});