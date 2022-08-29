import React from 'react';
import { Filter } from '../Filter';
import { shallow } from 'enzyme';
import { DatePicker } from '@9softstudio/react-datepicker';
import { defaultPagingOption } from '../../../../../modules/constants';

describe('feedback report - components - Filter ', () => {
    const rateList = [
        {
            RateId: 1,
            Checked: true,
            RateName: 'None'
        },
        {
            RateId: 2,
            Checked: true,
            RateName: 'Very Please'
        }
    ]
    const defaultProps = {
        rateList,
        today: "6/28/2019",
        pagingOption: defaultPagingOption
    }

    it('render correctly', () => {
        const result = shallow(<Filter {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with selectedRates is empty', () => {
        const result = shallow(<Filter {...defaultProps} />);
        result.setState({ selectedRates: '' });

        expect(result).toMatchSnapshot();
    });

    it("should call handleChange when change affiliate", () => {
        const mockedEvent = { target: { name: 'affName', value: "abc" } }
        const wrapper = shallow(<Filter {...defaultProps} />);

        wrapper.find('Input[type="text"][name="affName"]').simulate('change', mockedEvent);

        expect(wrapper.state('affName')).toEqual("abc");
    });

    it('handleChangeRate - should call setState', () => {
        const wrapper = shallow(<Filter {...defaultProps} />);
        const instance = wrapper.instance();
        instance.setState = jest.fn();

        instance.handleChangeRate(null, '1,2,3')

        expect(instance.setState).toHaveBeenCalledWith({ selectedRates: '1,2,3' });
    });

    it('handlePressEnter - should call hanldeSearch when press Enter', () => {
        const eventAffName = { charCode: 13, preventDefault: jest.fn() };

        const wrapper = shallow(<Filter {...defaultProps} />);
        const instance = wrapper.instance();
        instance.handleSearch = jest.fn();

        wrapper.find('Input[type="text"][name="affName"]').simulate('keyPress', eventAffName);

        expect(instance.handleSearch).toHaveBeenCalled();
    });

    it('handlePressEnter - should not call hanldeSearch when press other keyboards', () => {
        const eventAffName = { charCode: 123 };

        const wrapper = shallow(<Filter {...defaultProps} />);
        const instance = wrapper.instance();
        instance.handleSearch = jest.fn();

        wrapper.find('Input[type="text"][name="affName"]').simulate('keyPress', eventAffName);

        expect(instance.handleSearch).not.toHaveBeenCalled();
    });

    it('should call handleChangeFromDate when From change', () => {
        const monthDate = { format: jest.fn(() => "6/25/2019")};
        const wrapper = shallow(<Filter {...defaultProps} />);

        wrapper.setState = jest.fn(fn => fn && fn({toDate: "6/28/2019" }));

        wrapper.find(DatePicker).at(0).simulate('change', monthDate);

        expect(wrapper.setState).toHaveBeenCalledWith(expect.any(Function));
    });

    it('should call handleChangeToDate when To change', () => {
        const monthDate = { format: jest.fn(() => "06/26/2019")};
        const wrapper = shallow(<Filter {...defaultProps} />);

        wrapper.setState = jest.fn(fn => fn({ fromDate: "06/01/2019" }));

        wrapper.find(DatePicker).at(1).simulate('change', monthDate);

        expect(wrapper.setState).toHaveBeenCalledWith(expect.any(Function));
    });

    it('handleSearch - should be called when click Search Button', () => {
        const props = Object.assign({}, defaultProps, { setParam: jest.fn(), setReportTitle: jest.fn() });
        const wrapper = shallow(<Filter {...props} />);

        wrapper.find('Button').simulate('click');

        expect(props.setParam).toHaveBeenCalledWith({ AffName: "", FromDate: "06/01/2019", PageIndex: 1, PageSize: 20, RateIds: "1,2", ToDate: "06/28/2019" });
    });

    it('_buildReportTitle - should return correct title', () => {
        const wrapper = shallow(<Filter {...defaultProps} />);
        wrapper.setState({ affName: 'abc', selectedRates: '1' })

        const title = wrapper.instance()._buildReportTitle('abc', '1', "06/01/2019", "06/28/2019")

        expect(title).toEqual("FEEDBACK REPORT - Affiliate contains 'abc' - None - FROM 06/01/2019 TO 06/28/2019");
    });
})