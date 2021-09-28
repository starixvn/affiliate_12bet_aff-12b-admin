import React from 'react'
import AppContainer from '../../AppContainer'
import { shallow } from 'enzyme'
import configureMockStore from 'redux-mock-store'
import { SET_SORTED_PARAM } from '../../../actions/types'

const mockStore = configureMockStore()

describe('player detal - AppContainer - connect', () => {
    let wrapper, store

    beforeEach(() => {
        const initialState = {
            hasTurnOverPermission: true,
            playerDetail: {
                sortedParam: {}
            }
        }
        store = mockStore(initialState)
        wrapper = shallow(<AppContainer store={store} />).dive()
    })

    it('should show correct value', () => {
        const { hasTurnOverPermission, sortedParam } = wrapper.props();
        expect(sortedParam).toEqual({})
        expect(hasTurnOverPermission).toBeTruthy()
    })

    it('should call correct action', () => {
        wrapper.props().setSortedParam({ sortById: 123 })

        const actions = store.getActions();
        expect(actions).toEqual([{ type: SET_SORTED_PARAM, data: { sortById: 123 } }])
    })
})