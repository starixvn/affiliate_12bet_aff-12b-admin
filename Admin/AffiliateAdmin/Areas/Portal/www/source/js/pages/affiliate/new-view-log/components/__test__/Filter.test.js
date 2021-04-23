import React from 'react';
import { Filter } from '../Filter';
import { shallow } from 'enzyme';
import { DatePicker } from '@9softstudio/react-datepicker';

describe('new view log - components - Filter', () => {
    afterAll(() => {
        jest.resetAllMocks();
    });

    const today = '5/5/2022';
    const logActionList = [{ ActionIds: '123', ActionName: 'Test' }];
    const defaultProps = {
        today,
        selectedAff: '',
        mainViewParam: null,
        detailViewParam: null,
        getLogActionList: jest.fn(() => logActionList)
    };

    it('render correctly', () => {
        const result = shallow(<Filter {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with detail', () => {
        const props = Object.assign({}, defaultProps, { selectedAff: "12BetAFF001" });
        const result = shallow(<Filter {...props} />);
        result.instance().setState({ logActionList });

        expect(result).toMatchSnapshot();
    });

    it('componentDidUpdate - should call setState with main param', () => {
        const mainViewParam = { affName: '', logActionIds: '', fromDate: '12/5/2021', toDate: '5/5/2022' }
        const props = Object.assign({}, defaultProps, { mainViewParam });
        const result = shallow(<Filter {...props} />);
        const instance = result.instance();
        instance.setState = jest.fn();

        instance.componentDidUpdate({ ...defaultProps, selectedAff: "12BetAFF001" });

        expect(instance.setState).toHaveBeenCalledWith(mainViewParam);
    });

    it('componentDidUpdate - should call setState with view param', () => {
        const detailViewParam = { logActionIds: '', fromDate: '12/5/2021', toDate: '5/5/2022' };
        const props = Object.assign({}, defaultProps, { selectedAff: "12BetAFF001", detailViewParam });
        const result = shallow(<Filter {...props} />);
        const instance = result.instance();
        instance.setState = jest.fn();

        instance.componentDidUpdate(defaultProps);

        expect(instance.setState).toHaveBeenCalledWith(detailViewParam);
    });

    it('handleChange - should call setState when change Action select', () => {
        const event = {
            target: {
                type: 'select',
                value: 'Action Name',
                name: 'logActionIds'
            }
        }
        const wrapper = shallow(<Filter {...defaultProps} />);
        wrapper.instance().setState({ logActionList });
        wrapper.setState = jest.fn();

        wrapper.find('Input[type="select"][name="logActionIds"][id="logActionIds"]').simulate('change', event)

        expect(wrapper.setState).toHaveBeenCalledWith({ logActionIds: 'Action Name' });
    });

    it('handleChangeFromDate - should be called when From > To', () => {
        const monthDate = { format: jest.fn(() => "6/6/2022") };
        const wrapper = shallow(<Filter {...defaultProps} />);
        wrapper.instance().setState({ logActionList });
        wrapper.setState = jest.fn();

        wrapper.find(DatePicker).at(0).simulate('change', monthDate);

        expect(wrapper.setState).toHaveBeenCalledWith({ fromDate: "6/6/2022", toDate: "6/6/2022" });
    });

    it('handleChangeFromDate - should be called when From < To - rangDate', () => {
        const monthDate = { format: jest.fn(() => "6/6/2021") };
        const wrapper = shallow(<Filter {...defaultProps} />);
        wrapper.instance().setState({ logActionList });
        wrapper.setState = jest.fn();

        wrapper.find(DatePicker).at(0).simulate('change', monthDate);

        expect(wrapper.setState).toHaveBeenCalledWith({ fromDate: "6/6/2021", toDate: "12/6/2021" });
    });

    it('handleChangeFromDate - should be called when From into To - rangDate', () => {
        const monthDate = { format: jest.fn(() => "3/6/2022") };
        const wrapper = shallow(<Filter {...defaultProps} />);
        wrapper.instance().setState({ logActionList });
        wrapper.setState = jest.fn();

        wrapper.find(DatePicker).at(0).simulate('change', monthDate);

        expect(wrapper.setState).toHaveBeenCalledWith({ fromDate: "3/6/2022", toDate: "5/5/2022" });
    });

    it('handleChangeToDate - should be called when To < From', () => {
        const monthDate = { format: jest.fn(() => "12/12/2020") };
        const wrapper = shallow(<Filter {...defaultProps} />);
        wrapper.instance().setState({ logActionList });
        wrapper.setState = jest.fn();

        wrapper.find(DatePicker).at(1).simulate('change', monthDate);

        expect(wrapper.setState).toHaveBeenCalledWith({ fromDate: "12/12/2020", toDate: "12/12/2020" });
    });

    it('handleChangeToDate - should be called when To > From + arrangeDate', () => {
        const monthDate = { format: jest.fn(() => "12/12/2023") };
        const wrapper = shallow(<Filter {...defaultProps} />);
        wrapper.instance().setState({ logActionList });
        wrapper.setState = jest.fn();

        wrapper.find(DatePicker).at(1).simulate('change', monthDate);

        expect(wrapper.setState).toHaveBeenCalledWith({ fromDate: "6/12/2023", toDate: "12/12/2023" });
    });

    it('handleChangeToDate - should be called when To into From + arrangeDate', () => {
        const monthDate = { format: jest.fn(() => "3/12/2022") };
        const wrapper = shallow(<Filter {...defaultProps} />);
        wrapper.instance().setState({ logActionList });
        wrapper.setState = jest.fn();

        wrapper.find(DatePicker).at(1).simulate('change', monthDate);

        expect(wrapper.setState).toHaveBeenCalledWith({ fromDate: "11/5/2021", toDate: "3/12/2022" });
    });

    it('handleSearch - should call setMainViewParam', () => {
        const setMainViewParam = jest.fn();
        const props = Object.assign({}, defaultProps, { today: '3/26/2021', setMainViewParam });
        const wrapper = shallow(<Filter {...props} />);
        wrapper.instance().setState({ logActionList });
        wrapper.find('Button').simulate('click');

        expect(props.setMainViewParam).toHaveBeenCalledWith({ affName: '', logActionIds: '', fromDate: "3/25/2021", logActionIds: '', toDate: "3/26/2021" });
    });

    it('handleSearch - should call setDetailViewParam', () => {
        const setDetailViewParam = jest.fn();
        const props = Object.assign({}, defaultProps, { today: '3/26/2021', setDetailViewParam, selectedAff: "12BetAff001" });
        const wrapper = shallow(<Filter {...props} />);
        wrapper.instance().setState({ logActionList });
        wrapper.find('Button').simulate('click');

        expect(props.setDetailViewParam).toHaveBeenCalledWith({ logActionIds: '', fromDate: "3/25/2021", logActionIds: '', toDate: "3/26/2021" });
    });

    it('_handleKeyPress - should call _handleSubmit', () => {
        const event = { charCode: 13, preventDefault: jest.fn() };
        const result = shallow(<Filter {...defaultProps} />);
        const instance = result.instance();
        instance._handleSearch = jest.fn();

        instance._handleKeyPress(event)

        expect(instance._handleSearch).toHaveBeenCalled();
    });

    it('_handleKeyPress - should not call _handleSubmit', () => {
        const event = { charCode: 123, preventDefault: jest.fn() };
        const result = shallow(<Filter {...defaultProps} />);
        const instance = result.instance();
        instance._handleSearch = jest.fn();

        instance._handleKeyPress(event)

        expect(instance._handleSearch).not.toHaveBeenCalled();
    });
});