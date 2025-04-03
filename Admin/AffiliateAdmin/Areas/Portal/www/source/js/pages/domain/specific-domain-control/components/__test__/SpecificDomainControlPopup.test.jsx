import React from 'react';
import { SpecificDomainControlPopup } from '../SpecificDomainControlPopup';
import { shallow } from 'enzyme';

describe('specific domain control - components - SpecificDomainControlPopup ', () => {
    const defaultProps = {
        selectedDomainId: null,
        param: 'test',
        toggleLoader: jest.fn(),
        fetchData: jest.fn(),
        setSelectedDomainId: jest.fn(),
        getSpecificDomainById: jest.fn(),
        insertDomain: jest.fn(),
        updateDomain: jest.fn()
    };

    beforeEach(() => {
        jest.clearAllMocks();
    });

    it('render correctly', () => {
        const result = shallow(<SpecificDomainControlPopup {...defaultProps} />);
        expect(result).toMatchSnapshot();
    });

    it('should show popup when selectedDomainId is not null', () => {
        const props = { ...defaultProps, selectedDomainId: 1 };
        
        const wrapper = shallow(<SpecificDomainControlPopup {...props} />);
        wrapper.setState({
            selectedDomain: {
                domainControlId: 1,
                affDomain: 'test.com',
                redirectDomain: 'redirect.com',
                affDomainSSL: false,
                redirectDomainSSL: false,
                isUsed: true
            }
        });
        
        const modal = wrapper.find('Modal');
        expect(modal.exists()).toBe(true);
        expect(modal.prop('isOpen')).toBe(true);
    });

    it('should not show popup when selectedDomainId is null', () => {
        const wrapper = shallow(<SpecificDomainControlPopup {...defaultProps} />);
        wrapper.setState({
            selectedDomain: null
        });
        
        const modal = wrapper.find('Modal');
        expect(modal.exists()).toBe(false);
    });

    it('should handle input changes correctly', () => {
        const wrapper = shallow(<SpecificDomainControlPopup {...defaultProps} />);
        wrapper.setState({ 
            selectedDomain: { 
                domainControlId: 0,
                affDomain: '',
                affDomainSSL: false
            } 
        });

        // Test text input change
        wrapper.find('Input[name="affDomain"]').simulate('change', { 
            target: { 
                name: 'affDomain', 
                value: 'newdomain.com' 
            } 
        });

        expect(wrapper.state('selectedDomain').affDomain).toBe('newdomain.com');

        // Test checkbox change
        wrapper.find('Input[name="affDomainSSL"]').simulate('change', { 
            target: { 
                name: 'affDomainSSL', 
                type: 'checkbox',
                checked: true 
            } 
        });

        expect(wrapper.state('selectedDomain').affDomainSSL).toBe(true);
    });

    it('should handle affiliate names input changes correctly', () => {
        const wrapper = shallow(<SpecificDomainControlPopup {...defaultProps} />);
        wrapper.setState({ 
            isInsert: true,
            selectedDomain: { domainControlId: 0 },
            affNames: ''
        });

        const preventDefault = jest.fn();

        // Test valid input
        wrapper.find('Input[name="affNames"]').simulate('change', { 
            target: { value: 'test1,test2' },
            preventDefault
        });

        expect(wrapper.state('affNames')).toBe('test1,test2');
        expect(preventDefault).toHaveBeenCalled();

        // Test invalid input
        wrapper.find('Input[name="affNames"]').simulate('change', { 
            target: { value: 'test1@test2' },
            preventDefault
        });

        expect(wrapper.state('affNames')).toBe('test1,test2');
        expect(preventDefault).toHaveBeenCalled();
    });

    it('should handle affiliate names blur correctly', () => {
        const wrapper = shallow(<SpecificDomainControlPopup {...defaultProps} />);
        wrapper.setState({ 
            isInsert: true,
            selectedDomain: { domainControlId: 0 },
            affNames: 'test1, test2, test3, , test4' 
        });

        wrapper.find('Input[name="affNames"]').simulate('blur');

        expect(wrapper.state('affNames')).toBe('test1,test2,test3,test4');
    });

    it('should handle insert domain successfully', async () => {
        const mockResult = { IsSuccessful: true, Message: 'Success' };
        const props = {
            ...defaultProps,
            insertDomain: jest.fn().mockResolvedValue(mockResult)
        };
        const wrapper = shallow(<SpecificDomainControlPopup {...props} />);
        wrapper.setState({
            isInsert: true,
            selectedDomain: {
                domainControlId: 0,
                affDomain: 'test.com',
                redirectDomain: 'redirect.com',
                affDomainSSL: false,
                redirectDomainSSL: false,
                isUsed: false
            },
            affNames: '12betaff001,12betaff002'
        });

        await wrapper.instance()._handleSubmit();

        expect(props.toggleLoader).toHaveBeenCalledTimes(2);
        expect(props.insertDomain).toHaveBeenCalledWith({
            domainControlId: 0,
            affDomain: 'test.com',
            redirectDomain: 'redirect.com',
            affDomainSSL: false,
            redirectDomainSSL: false,
            isUsed: false,
            affName: '12betaff001,12betaff002'
        });
        expect(wrapper.state('isShowMessage')).toBe(true);
        expect(wrapper.state('isSuccess')).toBe(true);
    });

    it('should handle update domain successfully', async () => {
        const mockResult = { IsSuccessful: true, Message: 'Success' };
        const props = {
            ...defaultProps,
            updateDomain: jest.fn().mockResolvedValue(mockResult)
        };
        const wrapper = shallow(<SpecificDomainControlPopup {...props} />);
        wrapper.setState({
            isInsert: false,
            selectedDomain: {
                domainControlId: 1,
                affDomain: 'test.com',
                redirectDomain: 'redirect.com',
                affDomainSSL: true,
                redirectDomainSSL: false,
                isUsed: true
            }
        });

        await wrapper.instance()._handleSubmit();

        expect(props.toggleLoader).toHaveBeenCalledTimes(2);
        expect(props.updateDomain).toHaveBeenCalledWith({
            domainControlId: 1,
            affDomain: 'test.com',
            redirectDomain: 'redirect.com',
            affDomainSSL: true,
            redirectDomainSSL: false,
            isUsed: true
        });
        expect(wrapper.state('isShowMessage')).toBe(true);
        expect(wrapper.state('isSuccess')).toBe(true);
    });

    it('should handle domain validation failure', async () => {
        const wrapper = shallow(<SpecificDomainControlPopup {...defaultProps} />);
        wrapper.setState({
            isInsert: true,
            selectedDomain: {
                domainControlId: 0,
                affDomain: '',
                redirectDomain: '',
                affDomainSSL: false,
                redirectDomainSSL: false,
                isUsed: false
            },
            affNames: 'test1,test2'
        });

        await wrapper.instance()._handleSubmit();

        expect(wrapper.state('isShowMessage')).toBe(true);
        expect(wrapper.state('isSuccess')).toBe(false);
        expect(wrapper.state('message')).toBe('Affiliate(s) is an invalid format or affiliate!');
    });

    it('should handle affiliate names validation failure', async () => {
        const wrapper = shallow(<SpecificDomainControlPopup {...defaultProps} />);
        wrapper.setState({
            isInsert: true,
            selectedDomain: {
                domainControlId: 0,
                affDomain: 'test.com',
                redirectDomain: 'redirect.com',
                affDomainSSL: false,
                redirectDomainSSL: false,
                isUsed: false
            },
            affNames: ''
        });

        await wrapper.instance()._handleSubmit();

        expect(wrapper.state('isShowMessage')).toBe(true);
        expect(wrapper.state('isSuccess')).toBe(false);
        expect(wrapper.state('message')).toBe('Please enter Affiliate(s)!');
    });

    it('should handle close popup', () => {
        const wrapper = shallow(<SpecificDomainControlPopup {...defaultProps} />);
        wrapper.setState({
            isShowMessage: true,
            message: 'Test message',
            selectedDomain: { domainControlId: 1 }
        });

        wrapper.instance()._handleClosePopup();

        expect(wrapper.state('isShowMessage')).toBe(false);
        expect(wrapper.state('message')).toBe('');
        expect(wrapper.state('selectedDomain')).toBeNull();
        expect(defaultProps.setSelectedDomainId).toHaveBeenCalledWith(null);
    });

    it('should handle hide message with success', () => {
        const wrapper = shallow(<SpecificDomainControlPopup {...defaultProps} />);
        wrapper.setState({
            isShowMessage: true,
            isSuccess: true
        });

        wrapper.instance()._hideMessage(true);

        expect(wrapper.state('isShowMessage')).toBe(false);
        expect(defaultProps.fetchData).toHaveBeenCalledWith('test');
        expect(defaultProps.setSelectedDomainId).toHaveBeenCalledWith(null);
    });

    it('should handle hide message without success', () => {
        const wrapper = shallow(<SpecificDomainControlPopup {...defaultProps} />);
        wrapper.setState({
            isShowMessage: true,
            isSuccess: false
        });

        wrapper.instance()._hideMessage(false);

        expect(wrapper.state('isShowMessage')).toBe(false);
        expect(defaultProps.fetchData).not.toHaveBeenCalled();
        expect(defaultProps.setSelectedDomainId).not.toHaveBeenCalled();
    });
}); 