import React from 'react';
import { Filter } from '../Filter';
import { shallow } from 'enzyme';
import { DatePicker } from '@9softstudio/react-datepicker';

describe('view log - components - Filter', () => {
    afterAll(() => {
        jest.resetAllMocks();
    });

    const today = '12/27/2019';
    const logActionList = [{ ActionId: 123, ActionName: 'Test' }];
    const defaultProps = {
        today,
        getLogActionList: jest.fn(() => logActionList)
    };

    it('render correctly', () => {
        const result = shallow(<Filter {...defaultProps} />);
        result.instance().setState({ logActionList });
        expect(result).toMatchSnapshot();
    });

    it('handleChange - should call setState when change Action select', () => {
        const event = {
            target: {
                type: 'select',
                value: 'UserInfo',
                name: 'logAction'
            }
        }
        const wrapper = shallow(<Filter {...defaultProps} />);
        wrapper.instance().setState({ logActionList });
        wrapper.setState = jest.fn();

        wrapper.find('Input[type="select"][name="logAction"][id="logAction"]').simulate('change', event)

        expect(wrapper.setState).toHaveBeenCalledWith({ logAction: 'UserInfo' });
    });

    it('handleChangeFromDate - should be called when From change', () => {
        const monthDate = { format: jest.fn(() => "6/28/2019")};
        const wrapper = shallow(<Filter {...defaultProps} />);
        wrapper.instance().setState({ logActionList });
        wrapper.setState = jest.fn();

        wrapper.find(DatePicker).at(0).simulate('change', monthDate);

        expect(wrapper.setState).toHaveBeenCalledWith({ fromDate: "6/28/2019", toDate: "12/27/2019" });
    });

    it('handleChangeToDate - should be called when To change', () => {
        const monthDate = { format: jest.fn(() => "12/27/2019")};
        const wrapper = shallow(<Filter {...defaultProps} />);
        wrapper.instance().setState({ logActionList });
        wrapper.setState = jest.fn();

        wrapper.find(DatePicker).at(1).simulate('change', monthDate);

        expect(wrapper.setState).toHaveBeenCalledWith({ toDate: "12/27/2019" });
    });

    it('handleSearch - should be called when click Search Button', () => {
        const setParam = jest.fn();
        const props = Object.assign({}, defaultProps, { setParam, setShowIdList: jest.fn() });
        const wrapper = shallow(<Filter {...props} />);
        wrapper.instance().setState({ logActionList });
        wrapper.find('Button').simulate('click');

        expect(props.setParam).toHaveBeenCalledWith({ fromDate: "6/27/2019", logAction: 0, toDate: "12/27/2019" });
        expect(props.setShowIdList).toHaveBeenCalledWith({});
    });
});