var expect = require('chai').expect;
var arrangeBy = require('../arrangeBy');

describe('arrangeBy()', function() {
    it ('Given arrangeBy "name" THEN record with same "name" should be merged', function() {

        const input = [{
            id: 1,
            name: 'bob',
        }, {
            id: 2,
            name: 'sally',
        }, {
            id: 3,
            name: 'bob',
            age: 30,
        }]

        var expectedOutput = {
            bob: [{
                id: 1,
                name: 'bob'
            }, {
                id: 3,
                name: 'bob',
                age: 30
            }],
            sally: [{
                id: 2,
                name: 'sally'
            }]
        }

        const arrangeByName = arrangeBy('name')
        var output = arrangeByName(input)

        expect(output).to.deep.equal(expectedOutput);
    })
});