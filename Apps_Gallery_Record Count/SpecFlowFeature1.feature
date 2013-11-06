Feature: CheckAppSearchCount
         search number of apps returned 


Scenario: search applications for terms has expected counts
    Given alteryx is running at "http://gallery.alteryx.com"
    When I search for application at "api/apps/gallery/" with search term "choosing"
    Then I see record-count is 1
