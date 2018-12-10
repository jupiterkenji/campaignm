var expect = require('chai').expect;
var arrangeBy = require('../arrangeBy');

describe('Simple', function() {
    it ('Given arrangeBy "name" THEN record with same "name" should be merged', function() {

        var input = [{
            id: 1, name: 'bob',
        }, {
            id: 2, name: 'sally',
        }, {
            id: 3, name: 'bob', age: 30,
        }]

        const arrangeByName = arrangeBy('name')
        var output = arrangeByName(input)

        var expectedOutput = {
            bob: [{
                id: 1, name: 'bob'
            }, {
                id: 3, name: 'bob', age: 30
            }],
            sally: [{
                id: 2, name: 'sally'
            }]
        }
        expect(output).to.deep.equal(expectedOutput);

        const expectedInput = [{
            id: 1, name: 'bob',
        }, {
            id: 2, name: 'sally',
        }, {
            id: 3, name: 'bob', age: 30,
        }]
        expect(expectedInput).to.deep.equal(input);
    })
});

describe('Complex', function() {
    it ('Given arrangeBy "name" THEN record with same "name" should be merged', function() {

        const input = [{
            id: 1, name: 'bob',
        }, {
            id: 2, name: 'sally',
        }, {
            id: 3, name: 'bob', age: 30,
        }, {
            id: 4, name: 'sally2', age: 40,
        }, {
            id: 5, name: 'bob', age: 37,
        }]

        const arrangeByName = arrangeBy('name')
        var output = arrangeByName(input)

        const expectedOutput = {
            bob: [{
                id: 1, name: 'bob'
            }, {
                id: 3, name: 'bob', age: 30
            }, {
                id: 5, name: 'bob', age: 37
            }],
            sally: [{
                id: 2, name: 'sally'
            }],
            sally2: [{
                id: 4, name: 'sally2', age: 40
            }]
        }
        expect(output).to.deep.equal(expectedOutput);
    })
});


describe('key not exist in object', function() {
    it ('Given key "name" THEN record without key "name" should be exlude', function() {

        var input = [{
            id: 1, name: 'bob',
        }, {
            id: 2, name: 'sally',
        }, {
            id: 3, familyName: 'sally 3',
        }]

        const arrangeByName = arrangeBy('name')
        var output = arrangeByName(input)

        var expectedOutput = {
            bob: [{
                id: 1, name: 'bob'
            }],
            sally: [{
                id: 2, name: 'sally'
            }]
        }
        expect(output).to.deep.equal(expectedOutput);
    })
});

describe('Element is null/undefined or not an object', function() {
    it ('Given element is null/undefined THEN should be exlude', function() {

        var input = [{
            id: 1, name: 'bob',
        }, {
            id: 2, name: 'sally',
        }, {
            undefined,
        }, {
            id: 3, name: undefined,
        }]

        const arrangeByName = arrangeBy('name')
        var output = arrangeByName(input)

        var expectedOutput = {
            bob: [{
                id: 1, name: 'bob'
            }],
            sally: [{
                id: 2, name: 'sally'
            }]
        }
        expect(output).to.deep.equal(expectedOutput);
    })
});
