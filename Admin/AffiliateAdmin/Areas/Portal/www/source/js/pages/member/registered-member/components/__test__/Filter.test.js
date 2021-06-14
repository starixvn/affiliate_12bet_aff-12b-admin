import React from 'react';
import { Filter } from '../Filter';
import { shallow } from 'enzyme';

import { defaultPagingOption } from '../../../../../modules/constants';

describe('registerd member list - components - Filter ', () => {
    const defaultProps = {
        pagingOption: defaultPagingOption,
    }

    it('render correctly', () => {
        const result = shallow(<Filter {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });

    it('_handleSearch - should be called when click Search Button', () => {
        const setParam = jest.fn();
        const props = Object.assign({}, defaultProps, { setParam });
        const wrapper = shallow(<Filter {...props} />);

        wrapper.find('Button').simulate('click');

        expect(props.setParam).toHaveBeenCalledWith({ AffName: '', UserName: '', PageIndex: 1, PageSize: 20 });
    });

    it('setAffName - should change affName when change affiliate textbox', () => {
        const event = {
            target: { value: '12Bet' }
        };
        const wrapper = shallow(<Filter {...defaultProps} />);

        wrapper.find('Input[type="text"][name="affName"]').simulate('change', event);

        expect(wrapper.find('Input[type="text"][name="affName"]').prop('value')).toEqual('12Bet');
    });

    it('setUserName - should change userName when change user name textbox', () => {
        const event = {
            target: { value: 'abc' }
        };
        const wrapper = shallow(<Filter {...defaultProps} />);

        wrapper.find('Input[type="text"][name="userName"]').simulate('change', event);

        expect(wrapper.find('Input[type="text"][name="userName"]').prop('value')).toEqual('abc');
    });

    it('_handleKeyPress - should call _handleSearch when press enter in affiliate textbox', () => {
        const event = {
            charCode: 13,
            preventDefault: jest.fn()
        };
        const props = Object.assign({}, defaultProps, { setParam: jest.fn() })
        const wrapper = shallow(<Filter {...props} />);

        wrapper.find('Input[type="text"][name="affName"]').simulate('keyPress', event);

        expect(event.preventDefault).toHaveBeenCalled();
        expect(props.setParam).toHaveBeenCalledWith({"AffName": "", "PageIndex": 1, "PageSize": 20, "UserName": ""});
    });

    it('_handleKeyPress - should call _handleSearch when press enter in user name textbox', () => {
        const event = {
            charCode: 13,
            preventDefault: jest.fn()
        };
        const props = Object.assign({}, defaultProps, { setParam: jest.fn() })
        const wrapper = shallow(<Filter {...props} />);

        wrapper.find('Input[type="text"][name="userName"]').simulate('keyPress', event);

        expect(event.preventDefault).toHaveBeenCalled();
        expect(props.setParam).toHaveBeenCalledWith({"AffName": "", "PageIndex": 1, "PageSize": 20, "UserName": ""});
    });

    it('_handleKeyPress - should not call _handleSearch when press other keys', () => {
        const event = {
            charCode: 16,
            preventDefault: jest.fn()
        };
        const wrapper = shallow(<Filter {...defaultProps} />);

        wrapper.find('Input[type="text"][name="affName"]').simulate('keyPress', event);

        expect(event.preventDefault).not.toHaveBeenCalled();
    });
})