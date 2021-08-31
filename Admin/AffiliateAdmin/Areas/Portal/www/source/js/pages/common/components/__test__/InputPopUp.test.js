import React from 'react'
import { shallow, mount } from 'enzyme'
import InputPopUp from '../InputPopUp'
import { NotificationPopup } from "../NotificationPopup"

describe("common - Checkbox Select All component - ", () => {

    const defaultProps = {
        isOpen: true,
        title: 'test',
        value: 'test',
        toggle: jest.fn(),
        handleSubmit: jest.fn(),
    }

    it("render correctly", () => {
        const wrapper = shallow(<InputPopUp {...defaultProps} />)

        expect(wrapper).toMatchSnapshot()
    })

    it("toggleNested - should be called to set message when click Ok button", () => {
        const wrapper = mount(<InputPopUp {...defaultProps} />)

        wrapper.find('Button').at(0).simulate('click')
        expect(wrapper.find(NotificationPopup).props().message).toEqual("Are your sure?")

        wrapper.find('Button').at(0).simulate('click')
        expect(wrapper.find(NotificationPopup).props().message).toEqual("")
    })

    it("handleChangeText - should be called when change input", () => {
        const wrapper = mount(<InputPopUp {...defaultProps} />)
        const input = wrapper.find('Input[type="text"]')

        expect(input.props().value).toEqual("test")

        input.simulate('change', { target: { value: 'abc123' } })

        expect(wrapper.find('Input[type="text"]').props().value).toEqual("abc123")
    })

    it("handleConfirm - should be called when click Ok NotificationPopup ", () => {
        const wrapper = shallow(<InputPopUp {...defaultProps} />)

        wrapper.find(NotificationPopup).props().onClose(true)

        expect(defaultProps.handleSubmit).toHaveBeenCalled()
    })

    it("handleConfirm - should be called when click Cancel NotificationPopup ", () => {
        const wrapper = shallow(<InputPopUp {...defaultProps} />)

        wrapper.find(NotificationPopup).props().onClose(false)

        expect(defaultProps.handleSubmit).toHaveBeenCalled()
    })
})