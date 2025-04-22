import React from 'react';
import { Filter } from '../Filter';
import { shallow } from 'enzyme';

describe('domain control - components - Filter ', () => {
    const defaultProps = {
        marketList: [
            {
                LanguageCode: "en-GB",
                DomainName: "English (Euro)"
            },
            {
                LanguageCode: "vi-VN",
                DomainName: "VietNamese"
            }
        ],
        affSubDomainList: [
            {
                columnKey: "test",
                columnName: "test",
                checked: true
            }
        ]
    }

    it('render correctly', () => {
        const result = shallow(<Filter {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });

    it('_handleSearch - should be called when click Search Button', () => {
        const props = Object.assign({}, defaultProps, { setSelectedTargetCountry: jest.fn(), fetchData: jest.fn() });
        const wrapper = shallow(<Filter {...props} />);

        wrapper.find('Button').simulate('click');

        expect(props.setSelectedTargetCountry).toHaveBeenCalledWith('');
        expect(props.fetchData).toHaveBeenCalledWith('', defaultProps.affSubDomainList);
    });

    it('setMarket - should change category when change market select', () => {
        const event = {
            target: { value: 'en-GB' }
        };
        const wrapper = shallow(<Filter {...defaultProps} />);

        wrapper.find('Input[type="select"][name="market"]').simulate('change', event);

        expect(wrapper.find('Input[type="select"][name="market"]').prop('value')).toEqual('en-GB');
    });
})