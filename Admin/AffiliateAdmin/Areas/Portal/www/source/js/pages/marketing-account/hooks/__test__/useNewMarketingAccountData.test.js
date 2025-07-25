import React from 'react';
import { mount } from 'enzyme';
import useNewMarketingAccountData from '../useNewMarketingAccountData';
import * as actions from '../../actions';
import * as validation from '../../libs/validation';
import { defaultAdditionalInfo, defaultGeneralInfo, defaultPaymentInfo, defaultPersonalInfo, defaultWebsiteInfo } from '../../libs/constants';

// Mock the actions
jest.mock('../../actions', () => ({
    getMarketingAccountInfo: jest.fn(),
    createMarketingAccount: jest.fn(),
    getPaymentMethod: jest.fn(() => () => Promise.resolve(null))
}));

// Mock the validation
jest.mock('../../libs/validation', () => ({
    validationEmail: jest.fn()
}));

// Test component that uses the hook
const TestComponent = ({ affId, toggleLoader }) => {
    const hookResult = useNewMarketingAccountData(affId, toggleLoader);
    // Expose hook result for testing
    TestComponent.hookResult = hookResult;
    return <div data-testid="test-component">Test</div>;
};

describe('marketing-account - hooks - useNewMarketingAccountData', () => {
    let mockToggleLoader;

    beforeEach(() => {
        mockToggleLoader = jest.fn();
        jest.clearAllMocks();
        TestComponent.hookResult = null;
    });

    it('should initialize with default values', () => {
        mount(<TestComponent affId={null} toggleLoader={mockToggleLoader} />);

        expect(TestComponent.hookResult.resultInfo).toEqual({
            isSuccess: true,
            isShowMessage: false,
            message: ''
        });
        // The hook adds hasData: false when affId is null
        expect(TestComponent.hookResult.generalInfo).toEqual({
            ...defaultGeneralInfo,
            hasData: false
        });
        expect(TestComponent.hookResult.personalInfo).toEqual(defaultPersonalInfo);
        expect(TestComponent.hookResult.additionalInfo).toEqual(defaultAdditionalInfo);
        expect(TestComponent.hookResult.paymentInfo).toEqual(defaultPaymentInfo);
        expect(TestComponent.hookResult.websiteInfo).toEqual(defaultWebsiteInfo);
        expect(TestComponent.hookResult.minimumPayment).toBe(0);
        expect(TestComponent.hookResult.paymentMethods).toEqual([]);
    });

    it('should provide state setter functions', () => {
        mount(<TestComponent affId={null} toggleLoader={mockToggleLoader} />);

        expect(typeof TestComponent.hookResult.setGeneralInfo).toBe('function');
        expect(typeof TestComponent.hookResult.setPersonalInfo).toBe('function');
        expect(typeof TestComponent.hookResult.setAdditionalInfo).toBe('function');
        expect(typeof TestComponent.hookResult.setPaymentInfo).toBe('function');
        expect(typeof TestComponent.hookResult.setWebsiteInfo).toBe('function');
        expect(typeof TestComponent.hookResult.handleSubmit).toBe('function');
    });

    it('should fetch payment methods on mount', (done) => {
        const mockPaymentResponse = {
            MinAmount: 100,
            PaymentMethods: [
                { Key: 'paypal', Value: 'PayPal' },
                { Key: 'bank', Value: 'Bank Transfer' }
            ]
        };

        // Fix: getPaymentMethod returns a function that returns a Promise
        actions.getPaymentMethod.mockReturnValue(() => Promise.resolve(mockPaymentResponse));

        mount(<TestComponent affId={null} toggleLoader={mockToggleLoader} />);

        expect(mockToggleLoader).toHaveBeenCalledWith(true);

        // Wait for async operations
        setTimeout(() => {
            expect(actions.getPaymentMethod).toHaveBeenCalled();
            expect(TestComponent.hookResult.minimumPayment).toBe(100);
            expect(TestComponent.hookResult.paymentMethods).toEqual(mockPaymentResponse.PaymentMethods);
            expect(mockToggleLoader).toHaveBeenCalledWith(false);
            done();
        }, 100);
    });

    it('should handle payment method fetch failure gracefully', (done) => {
        // Fix: getPaymentMethod returns a function that returns a Promise resolving to null
        actions.getPaymentMethod.mockReturnValue(() => Promise.resolve(null));

        mount(<TestComponent affId={null} toggleLoader={mockToggleLoader} />);

        setTimeout(() => {
            expect(TestComponent.hookResult.minimumPayment).toBe(0);
            expect(TestComponent.hookResult.paymentMethods).toEqual([]);
            done();
        }, 100);
    });

    it('should set hasData to false when affId is null', () => {
        mount(<TestComponent affId={null} toggleLoader={mockToggleLoader} />);

        expect(TestComponent.hookResult.generalInfo.hasData).toBe(false);
    });

    it('should handle successful form submission', async () => {
        validation.validationEmail.mockReturnValue({ isValid: true, message: '' });
        actions.createMarketingAccount.mockResolvedValue({ isSuccessful: true });

        const wrapper = mount(<TestComponent affId={null} toggleLoader={mockToggleLoader} />);

        // Set required email
        TestComponent.hookResult.setPersonalInfo({ ...defaultPersonalInfo, email: 'test@example.com' });
        wrapper.update();

        const mockModalBodyRef = { current: { scrollTo: jest.fn() } };
        const mockSuccessCallback = jest.fn();

        await TestComponent.hookResult.handleSubmit(mockModalBodyRef, mockSuccessCallback);

        expect(validation.validationEmail).toHaveBeenCalledWith('test@example.com', true);
        expect(actions.createMarketingAccount).toHaveBeenCalled();
        expect(TestComponent.hookResult.resultInfo.isSuccess).toBe(true);
        expect(TestComponent.hookResult.resultInfo.isShowMessage).toBe(true);
        expect(TestComponent.hookResult.resultInfo.message).toBe('Successfully added a new marketing account');
        expect(mockSuccessCallback).toHaveBeenCalled();
    });

    it('should handle form submission with missing email', async () => {
        const wrapper = mount(<TestComponent affId={null} toggleLoader={mockToggleLoader} />);

        const mockModalBodyRef = { current: { scrollTo: jest.fn() } };

        await TestComponent.hookResult.handleSubmit(mockModalBodyRef);

        expect(TestComponent.hookResult.resultInfo.isSuccess).toBe(false);
        expect(TestComponent.hookResult.resultInfo.isShowMessage).toBe(true);
        expect(TestComponent.hookResult.resultInfo.message).toBe('Email Address is required');
        expect(mockModalBodyRef.current.scrollTo).toHaveBeenCalledWith({ top: 0, behavior: 'smooth' });
    });

    it('should handle form submission with invalid email', async () => {
        validation.validationEmail.mockReturnValue({ isValid: false, message: 'Email Address is invalid' });

        const wrapper = mount(<TestComponent affId={null} toggleLoader={mockToggleLoader} />);

        // Set invalid email
        TestComponent.hookResult.setPersonalInfo({ ...defaultPersonalInfo, email: 'invalid-email' });
        wrapper.update();

        const mockModalBodyRef = { current: { scrollTo: jest.fn() } };

        await TestComponent.hookResult.handleSubmit(mockModalBodyRef);

        expect(validation.validationEmail).toHaveBeenCalledWith('invalid-email', true);
        expect(TestComponent.hookResult.resultInfo.isSuccess).toBe(false);
        expect(TestComponent.hookResult.resultInfo.isShowMessage).toBe(true);
        expect(TestComponent.hookResult.resultInfo.message).toBe('Email Address is invalid');
    });

    it('should handle website form submission with hasWebSite true', async () => {
        validation.validationEmail.mockReturnValue({ isValid: true, message: '' });
        actions.createMarketingAccount.mockResolvedValue({ isSuccessful: true });

        const wrapper = mount(<TestComponent affId={null} toggleLoader={mockToggleLoader} />);

        TestComponent.hookResult.setPersonalInfo({ ...defaultPersonalInfo, email: 'test@example.com' });
        TestComponent.hookResult.setWebsiteInfo({ 
            ...defaultWebsiteInfo, 
            hasWebSite: true,
            siteUrl: 'https://example.com',
            noSiteDesc: 'This should be cleared'
        });
        wrapper.update();

        await TestComponent.hookResult.handleSubmit();

        expect(actions.createMarketingAccount).toHaveBeenCalledWith(
            expect.objectContaining({
                hasWebSite: true,
                siteUrl: 'https://example.com',
                noSiteDesc: ''
            })
        );
    });

    it('should handle website form submission with hasWebSite false', async () => {
        validation.validationEmail.mockReturnValue({ isValid: true, message: '' });
        actions.createMarketingAccount.mockResolvedValue({ isSuccessful: true });

        const wrapper = mount(<TestComponent affId={null} toggleLoader={mockToggleLoader} />);

        TestComponent.hookResult.setPersonalInfo({ ...defaultPersonalInfo, email: 'test@example.com' });
        TestComponent.hookResult.setWebsiteInfo({ 
            ...defaultWebsiteInfo, 
            hasWebSite: false,
            siteUrl: 'https://should-be-cleared.com',
            moreSiteUrls: 'should-be-cleared',
            noSiteDesc: 'No website description'
        });
        wrapper.update();

        await TestComponent.hookResult.handleSubmit();

        expect(actions.createMarketingAccount).toHaveBeenCalledWith(
            expect.objectContaining({
                hasWebSite: false,
                siteUrl: '',
                moreSiteUrls: '',
                noSiteDesc: 'No website description'
            })
        );
    });

    it('should handle state updates correctly', () => {
        const wrapper = mount(<TestComponent affId={null} toggleLoader={mockToggleLoader} />);

        TestComponent.hookResult.setGeneralInfo({ ...defaultGeneralInfo, targetCountryCode: 'US' });
        wrapper.update();

        expect(TestComponent.hookResult.generalInfo.targetCountryCode).toBe('US');

        TestComponent.hookResult.setPersonalInfo({ ...defaultPersonalInfo, firstName: 'John' });
        wrapper.update();

        expect(TestComponent.hookResult.personalInfo.firstName).toBe('John');
    });

    it('should handle modal scroll when modalBodyRef is not provided', async () => {
        const wrapper = mount(<TestComponent affId={null} toggleLoader={mockToggleLoader} />);

        await TestComponent.hookResult.handleSubmit(null);

        // Should not throw error when modalBodyRef is null
        expect(TestComponent.hookResult.resultInfo.isShowMessage).toBe(true);
    });

    it('should handle modal scroll when modalBodyRef.current is null', async () => {
        const wrapper = mount(<TestComponent affId={null} toggleLoader={mockToggleLoader} />);

        const mockModalBodyRef = { current: null };

        await TestComponent.hookResult.handleSubmit(mockModalBodyRef);

        // Should not throw error when modalBodyRef.current is null
        expect(TestComponent.hookResult.resultInfo.isShowMessage).toBe(true);
    });
}); 