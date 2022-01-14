import React from 'react';
import { Filter } from '../Filter';
import { shallow, mount } from 'enzyme';

import { defaultPagingOption } from '../../../../../modules/constants';

describe('offer aff - components - Filter ', () => {
    const defaultProps = {
        pagingOption: defaultPagingOption,
        isFromAnotherPage: false,
        offerFile: ''
    }

    it('render correctly', () => {
        const result = shallow(<Filter {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });

    it('useEffect - should call setIsFromOtherPages, setParam', () => {
        const props = Object.assign({}, defaultProps, { isFromAnotherPage: true, setIsFromAnotherPage: jest.fn(), setParam: jest.fn(), offerFile: 'abc.html' });

        mount(<Filter {...props} />);

        expect(props.setIsFromAnotherPage).toHaveBeenCalledWith(false);
        expect(props.setParam).toHaveBeenCalledWith({ affName: '', fileName: 'abc.html', pageIndex: 1, pageSize: 20 });
    });

    it('_handleSearch - should be called when click Search Button', () => {
        const setParam = jest.fn();
        const props = Object.assign({}, defaultProps, { setParam });
        const wrapper = shallow(<Filter {...props} />);

        wrapper.find('Button').simulate('click');

        expect(props.setParam).toHaveBeenCalledWith({ affName: '', fileName: '', pageIndex: 1, pageSize: 20 });
    });

    it('setAffName - should change affName when change affiliate textbox', () => {
        const event = {
            target: { value: '12Bet' }
        };
        const wrapper = shallow(<Filter {...defaultProps} />);

        wrapper.find('Input[type="text"][name="affName"]').simulate('change', event);

        expect(wrapper.find('Input[type="text"][name="affName"]').prop('value')).toEqual('12Bet');
    });

    it('setOfferFile - should change offerFile when change Offer Page HTML File textbox', () => {
        const event = {
            target: { value: 'abc' }
        };
        const wrapper = shallow(<Filter {...defaultProps} />);

        wrapper.find('Input[type="text"][name="offerFile"]').simulate('change', event);

        expect(wrapper.find('Input[type="text"][name="offerFile"]').prop('value')).toEqual('abc');
    });

    it('_handlePressEnter - should call _handleSearch when press enter', () => {
        const event = {
            charCode: 13,
            preventDefault: jest.fn()
        };
        const props = Object.assign({}, defaultProps, { setParam: jest.fn() })
        const wrapper = shallow(<Filter {...props} />);

        wrapper.find('Input[type="text"][name="affName"]').simulate('keyPress', event);

        expect(event.preventDefault).toHaveBeenCalled();
        expect(props.setParam).toHaveBeenCalledWith({ affName: '', fileName: '', pageIndex: 1, pageSize: 20 });
    });

    it('_handlePressEnter - should not call _handleSearch when press other keys', () => {
        const event = {
            charCode: 16,
            preventDefault: jest.fn()
        };
        const wrapper = shallow(<Filter {...defaultProps} />);

        wrapper.find('Input[type="text"][name="affName"]').simulate('keyPress', event);

        expect(event.preventDefault).not.toHaveBeenCalled();
    });
})