import React from 'react';
import { Filter } from '../Filter';
import { shallow } from 'enzyme';

describe('domain control - components - Filter ', () => {
    const defaultProps = {
        targetCountryList: [
            {
                LanguageCode: "en-GB",
                DomainName: "English (Euro)"
            },
            {
                LanguageCode: "vi-VN",
                DomainName: "VietNamese"
            }
        ]
    }

    it('render correctly', () => {
        const result = shallow(<Filter {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });

    it('_handleSearch - should be called when click Search Button', () => {
        const props = Object.assign({}, defaultProps, { setSelectedTargetCountryCode: jest.fn(), fetchData: jest.fn() });
        const wrapper = shallow(<Filter {...props} />);

        wrapper.find('Button').simulate('click');

        expect(props.setSelectedTargetCountryCode).toHaveBeenCalledWith('');
        expect(props.fetchData).toHaveBeenCalledWith('');
    });

    it('setTargetCountry - should change category when change targetCountry select', () => {
        const event = {
            target: { value: 'en-GB' }
        };
        const wrapper = shallow(<Filter {...defaultProps} />);

        wrapper.find('Input[type="select"][name="targetCountry"]').simulate('change', event);

        expect(wrapper.find('Input[type="select"][name="targetCountry"]').prop('value')).toEqual('en-GB');
    });
})