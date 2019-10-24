import React from 'react';
import { Filter } from '../Filter';
import { shallow, mount } from 'enzyme';

import { defaultPagingOption } from '../../../../../modules/constants';

describe('active member - components - Filter ', () => {
    it('render correctly', () => {
        const result = shallow(<Filter />);

        expect(result).toMatchSnapshot();
    });

    it('_handleSearch - should be called when click Search Button', () => {
        const props = Object.assign({}, { setMessage: jest.fn() });
        const wrapper = shallow(<Filter {...props} />);

        wrapper.find('Button').simulate('click');

        expect(props.setMessage).toHaveBeenCalledWith('Please input Deposit User Name!');
    });

    it('_handleSearch - should call _handleFetchData when click Search Button', () => {
        const props = Object.assign({}, { fetchActivatedData: jest.fn(), fetchUnactivatedData: jest.fn(), setShowText: jest.fn() });
        const wrapper = shallow(<Filter {...props} />);

        wrapper.find('Input[type="text"]').simulate('change', { target: { value: 'xyl157 ' }});
        wrapper.find('Button').simulate('click');

        expect(props.fetchActivatedData).toHaveBeenCalledWith({ userNames: 'xyl157' });
        //expect(props.fetchUnactivatedData).toHaveBeenCalledWith({ userNames: 'xyl157' });
        //expect(props.setShowText).toHaveBeenCalledWith(true);
    });

    it('_handleFetchData - should call fetchActivatedData, fetchUnactivatedData and setShowText', async () => {
        const props = Object.assign({}, { fetchActivatedData: jest.fn(() => 0), fetchUnactivatedData: jest.fn(() => 0), setShowText: jest.fn() });
        const wrapper = shallow(<Filter {...props} />);

        wrapper.find('Input[type="text"]').simulate('change', { target: { value: 'xyl157 ' }});
        await wrapper.find('Button').simulate('click');

        expect(props.fetchActivatedData).toHaveBeenCalledWith({ userNames: 'xyl157' });
        expect(props.fetchUnactivatedData).toHaveBeenCalledWith({ userNames: 'xyl157' });
        //expect(props.setShowText).toHaveBeenCalledWith(true);
    });

    it('_handleFetchData - should call fetchActivatedData, fetchUnactivatedData and setShowText', async () => {
        const props = Object.assign({}, { fetchActivatedData: jest.fn(() => 1), fetchUnactivatedData: jest.fn(() => 1), setShowText: jest.fn() });
        const wrapper = shallow(<Filter {...props} />);

        wrapper.find('Input[type="text"]').simulate('change', { target: { value: 'xyl157 ' }});
        await wrapper.find('Button').simulate('click');

        expect(props.fetchActivatedData).toHaveBeenCalledWith({ userNames: 'xyl157' });
        expect(props.fetchUnactivatedData).toHaveBeenCalledWith({ userNames: 'xyl157' });
        //expect(props.setShowText).toHaveBeenCalledWith(true);
    });

    it('_handlePressEnter - should call _handleSearch when press enter in input', () => {
        const event = {
            charCode: 13,
            preventDefault: jest.fn()
        };
        const props = Object.assign({}, { setMessage: jest.fn() })
        const wrapper = shallow(<Filter {...props} />);

        wrapper.find('Input[type="text"]').simulate('keyPress', event);

        expect(event.preventDefault).toHaveBeenCalled();
        expect(props.setMessage).toHaveBeenCalledWith('Please input Deposit User Name!');
    });

    it('_handlePressEnter - should not call _handleSearch when press other keys', () => {
        const event = {
            charCode: 16,
            preventDefault: jest.fn()
        };
        const wrapper = shallow(<Filter />);

        wrapper.find('Input[type="text"]').simulate('keyPress', event);

        expect(event.preventDefault).not.toHaveBeenCalled();
    });
})