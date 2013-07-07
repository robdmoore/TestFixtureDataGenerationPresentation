Test Fixture Data Generation Presentation
=========================================

Title
-----
Dear God not Object Mother: An exploration of good ways to generate test fixture data...

Description
-----------

Presentation that outlines some ideas I have about better ways of generating fixture data for testing (unit, integration or to insert int oa database for manual testing).

This presentation was first delivered to the [Perth .NET User Group](http://perthdotnet.blogspot.com.au/2013/06/july-meeting-small-talks.html) on July 4th 2013.

Notes
-----

* The theory behind the content presented here is explained in my [blog post](http://robdmoore.id.au/blog/2013/05/26/test-data-generation-the-right-way-object-mother-test-data-builders-nsubstitute-nbuilder/)
* The library I used in this example is one I created; [NTestDataBuilder](https://github.com/robdmoore/NTestDataBuilder)
* This is deliberately a fairly contrived example
* There are some things in here that aren't practical in a production codebase and some things I wouldn't normally do
* I've tried to get a balance between simplifying things to a point they are easily understandable at a glance, but complex enough that they give a good representation of what can happen in a representative codebase - if you have any suggestions of any improvements I can make please raise an issue
* I haven't covered everything that I normally would with tests; in this case I have unit tested the `Contains` method in the `Demographic` class and "integration" tested the `GetProductsForMember` query (usually I would do this against the actual database, but in this case it's just an in-memory collection to reduce the noise of the codebase)
* Yes, the "`ISession`" is mimicing NHibernate (the ORM I generally find myself using)
* No, the domain entities aren't "NHibernate-ready" - they don't have an id or virtual properties/methods
