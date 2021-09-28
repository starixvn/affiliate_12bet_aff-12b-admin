import React from 'react'
import { shallow, mount } from 'enzyme'
import CheckboxSelectAll from '../CheckboxSelectAll'

describe("common - Checkbox Select All component - ", () => {

    const defaultProps = {
        checkedList: { 123: true, 234: false },
        setShouldResetScrollPosition: jest.fn(),
        setCheckedList: jest.fn(),
    }

    it("render correctly", () => {
        const wrapper = shallow(<CheckboxSelectAll {...defaultProps} />)

        expect(wrapper).toMatchSnapshot();
    })

    it("_onCheckAll - should be called when click checkbox", () => {
        const wrapper = mount(<CheckboxSelectAll {...defaultProps} />)

        wrapper.find('input[type="checkbox"]').simulate('change')

        expect(defaultProps.setShouldResetScrollPosition).toHaveBeenCalledWith(false);
        expect(defaultProps.setCheckedList).toHaveBeenCalledWith({ 123: true, 234: true });
    })
})