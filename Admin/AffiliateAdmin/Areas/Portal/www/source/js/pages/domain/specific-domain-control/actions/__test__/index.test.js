import { post } from '../../../../../modules/ajaxRequest';
import { toggleLoader } from '../../../../../actions/loaderAction';
import {
    setSelectedDomainId,
    setParam,
    setShouldResetScrollPosition,
    setGridData,
    fetchData,
    getSpecificDomainById,
    insertSpecificDomain,
    updateSpecificDomain,
    deleteSpecificDomain
} from '../index';
import {
    SET_GRID_DATA,
    SET_PARAM,
    SET_SELECTED_SPECIFIC_DOMAIN_ID,
    SHOULD_RESET_SCROLL_POSITION
} from '../types';

jest.mock('../../../../../modules/ajaxRequest', () => ({
    post: jest.fn()
}));

jest.mock('../../../../../actions/loaderAction', () => ({
    toggleLoader: jest.fn()
}));

describe('Specific Domain Control Actions', () => {
    describe('Action Creators', () => {
        it('should create an action to set selected domain ID', () => {
            const data = '123';
            const expectedAction = {
                type: SET_SELECTED_SPECIFIC_DOMAIN_ID,
                data
            };
            expect(setSelectedDomainId(data)).toEqual(expectedAction);
        });

        it('should create an action to set param', () => {
            const data = { page: 1 };
            const expectedAction = {
                type: SET_PARAM,
                data
            };
            expect(setParam(data)).toEqual(expectedAction);
        });

        it('should create an action to set should reset scroll position', () => {
            const data = true;
            const expectedAction = {
                type: SHOULD_RESET_SCROLL_POSITION,
                data
            };
            expect(setShouldResetScrollPosition(data)).toEqual(expectedAction);
        });

        it('should create an action to set grid data', () => {
            const data = [{ id: 1 }];
            const expectedAction = {
                type: SET_GRID_DATA,
                data
            };
            expect(setGridData(data)).toEqual(expectedAction);
        });
    });

    describe('Async Actions', () => {
        beforeEach(() => {
            jest.clearAllMocks();
        });

        it('should fetch data successfully', async () => {
            const mockResponse = {
                status: 200,
                data: [{ id: 1 }]
            };
            post.mockResolvedValueOnce(mockResponse);

            const dispatch = jest.fn();
            const param = { page: 1 };

            await fetchData(param)(dispatch);

            expect(dispatch).toHaveBeenCalledWith(toggleLoader(true));
            expect(post).toHaveBeenCalledWith('Portal/SpecificDomainControl/GetSpecificDomainControlList', param);
            expect(dispatch).toHaveBeenCalledWith(setGridData(mockResponse.data));
            expect(dispatch).toHaveBeenCalledWith(setShouldResetScrollPosition(true));
            expect(dispatch).toHaveBeenCalledWith(toggleLoader(false));
        });

        it('should handle fetch data failure', async () => {
            const mockResponse = {
                status: 500,
                data: null
            };
            post.mockResolvedValueOnce(mockResponse);

            const dispatch = jest.fn();
            const param = { page: 1 };

            await fetchData(param)(dispatch);

            expect(dispatch).toHaveBeenCalledWith(toggleLoader(true));
            expect(post).toHaveBeenCalledWith('Portal/SpecificDomainControl/GetSpecificDomainControlList', param);
            expect(dispatch).toHaveBeenCalledWith(setGridData([]));
            expect(dispatch).toHaveBeenCalledWith(setShouldResetScrollPosition(true));
            expect(dispatch).toHaveBeenCalledWith(toggleLoader(false));
        });

        it('should get specific domain by ID successfully', async () => {
            const mockResponse = {
                status: 200,
                data: { id: 1 }
            };
            post.mockResolvedValueOnce(mockResponse);

            const result = await getSpecificDomainById('123')();

            expect(post).toHaveBeenCalledWith('Portal/SpecificDomainControl/GetSpecificDomainControlById', { domainControlId: '123' });
            expect(result).toEqual(mockResponse.data);
        });

        it('should get specific domain by ID with failure', async () => {
            const mockResponse = {
                status: 500,
                data: null
            };
            post.mockResolvedValueOnce(mockResponse);

            const result = await getSpecificDomainById('123')();

            expect(post).toHaveBeenCalledWith('Portal/SpecificDomainControl/GetSpecificDomainControlById', { domainControlId: '123' });
            expect(result).toBeNull();
        });

        it('should insert specific domain successfully', async () => {
            const mockResponse = {
                status: 200,
                data: { id: 1 }
            };
            post.mockResolvedValueOnce(mockResponse);

            const param = { name: 'Test Domain' };
            const result = await insertSpecificDomain(param)();

            expect(post).toHaveBeenCalledWith('Portal/SpecificDomainControl/InsertSpecificDomainControl', param);
            expect(result).toEqual(mockResponse.data);
        });

        it('should insert specific domain with failure', async () => {
            const mockResponse = {
                status: 500,
                data: null
            };
            post.mockResolvedValueOnce(mockResponse);

            const param = { name: 'Test Domain' };
            const result = await insertSpecificDomain(param)();

            expect(post).toHaveBeenCalledWith('Portal/SpecificDomainControl/InsertSpecificDomainControl', param);
            expect(result).toBeNull();
        });

        it('should update specific domain successfully', async () => {
            const mockResponse = {
                status: 200,
                data: { id: 1 }
            };
            post.mockResolvedValueOnce(mockResponse);

            const param = { id: 1, name: 'Updated Domain' };
            const result = await updateSpecificDomain(param)();

            expect(post).toHaveBeenCalledWith('Portal/SpecificDomainControl/UpdateSpecificDomainControl', param);
            expect(result).toEqual(mockResponse.data);
        });

        it('should update specific domain with failure', async () => {
            const mockResponse = {
                status: 500,
                data: null
            };
            post.mockResolvedValueOnce(mockResponse);

            const param = { id: 1, name: 'Updated Domain' };
            const result = await updateSpecificDomain(param)();

            expect(post).toHaveBeenCalledWith('Portal/SpecificDomainControl/UpdateSpecificDomainControl', param);
            expect(result).toBeNull();
        });

        it('should delete specific domain successfully', async () => {
            const mockResponse = {
                status: 200,
                data: { success: true }
            };
            post.mockResolvedValueOnce(mockResponse);

            const param = { id: 1 };
            const result = await deleteSpecificDomain(param)();

            expect(post).toHaveBeenCalledWith('Portal/SpecificDomainControl/DeleteSpecificDomainControl', param);
            expect(result).toEqual(mockResponse.data);
        });

        it('should delete specific domain with failure', async () => {
            const mockResponse = {
                status: 500,
                data: null
            };
            post.mockResolvedValueOnce(mockResponse);

            const param = { id: 1 };
            const result = await deleteSpecificDomain(param)();

            expect(post).toHaveBeenCalledWith('Portal/SpecificDomainControl/DeleteSpecificDomainControl', param);
            expect(result).toBeNull();
        });
    });
}); 